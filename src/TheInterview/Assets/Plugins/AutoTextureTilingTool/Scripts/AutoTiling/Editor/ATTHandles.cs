using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AutoTiling {

	public class ATTHandles {

		public static int lastDragHandleId;

		static int TexturePositionHandleXHash = "TexturePositionHandleX".GetHashCode();
//		static int TexturePositionHandleYHash = "TexturePositionHandleY".GetHashCode();
		static Vector2 dragHandleMousePos;
		static Vector3 positionInWorld;
//		static float currentRotation;
		static bool mouseDown = false;
		static bool holdingCtrl = false;

		static float stepBuffer;
		static float rotationStepStartPoint;
#if UNITY_5_6_OR_NEWER
        public static float TexturePositionHandle(Vector3 position, Quaternion rotation, float handleSize, Handles.CapFunction capfunc) {
#else
        public static float TexturePositionHandle(Vector3 position, Quaternion rotation, float handleSize, Handles.DrawCapFunction capfunc) {
#endif

            int id = GUIUtility.GetControlID(TexturePositionHandleXHash, FocusType.Passive);
			lastDragHandleId = id;
			Vector3 startScreenPosition = Handles.matrix.MultiplyPoint(position);
			Vector3 pointerScreenPosition = Handles.matrix.MultiplyPoint(position + (rotation * Vector3.forward * handleSize));
//			Vector3 screenDirectionVector = (pointerScreenPosition - startScreenPosition);
			Matrix4x4 cachedMatrix = Handles.matrix;
			
			float returnValue = 0f;
			
			switch (Event.current.GetTypeForControl(id)) {
			case EventType.MouseDown:
				if (HandleUtility.nearestControl == id && Event.current.button == 0) {
					GUIUtility.hotControl = id;
					dragHandleMousePos = Event.current.mousePosition;
					Event.current.Use();
					EditorGUIUtility.SetWantsMouseJumping(1);
					
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == id && Event.current.button == 0) {
					GUIUtility.hotControl = 0;
					Event.current.Use ();
					EditorGUIUtility.SetWantsMouseJumping(0);
				}
				break;
			case EventType.KeyDown:
				if (!holdingCtrl) {
					if (Event.current.keyCode == KeyCode.LeftControl) {
						holdingCtrl = true;
						stepBuffer = 0f;
					}
					else {
						holdingCtrl = false;
						stepBuffer = 0f;
					}
				}
				break;
			case EventType.KeyUp:
				holdingCtrl = false;
				stepBuffer = 0f;
				break;
			case EventType.MouseDrag:
				if (GUIUtility.hotControl == id) {
					Vector2 newMousePos = Event.current.mousePosition;
					Vector2 dragDelta = newMousePos - dragHandleMousePos;
					dragDelta.y *= -1f;
					Vector2 screenDirectionVector = Camera.current.WorldToScreenPoint(pointerScreenPosition) - Camera.current.WorldToScreenPoint(startScreenPosition);
					if ((screenDirectionVector + dragDelta).sqrMagnitude > screenDirectionVector.sqrMagnitude) { 
						returnValue = (newMousePos - dragHandleMousePos).magnitude / 500f;
					}
					else {
						returnValue = (newMousePos - dragHandleMousePos).magnitude / (-500f);
					}
					if (holdingCtrl) {
						stepBuffer += returnValue;
						if (Mathf.Abs(stepBuffer) >= TextureTilingEditorWindow.offsetStepSize) {
							returnValue = TextureTilingEditorWindow.offsetStepSize * Mathf.Sign(stepBuffer);
							stepBuffer = 0f;
						}
						else {
							returnValue = 0f;
						}
					}
					dragHandleMousePos = newMousePos;
					GUI.changed = true;
					Event.current.Use();
				}
				break;
			case EventType.Repaint:
				Color currentColor = Handles.color;
				if (GUIUtility.hotControl == id) {
					Handles.color = Handles.selectedColor;
				}
				Handles.matrix = Matrix4x4.identity;
#if UNITY_5_6_OR_NEWER
				capfunc(id, startScreenPosition, rotation, handleSize, EventType.Repaint);
#else
                capfunc(id, startScreenPosition, rotation, handleSize);
#endif
                Handles.matrix = cachedMatrix;
				Handles.color = currentColor;
				break;
			case EventType.Layout:
				Handles.matrix = Matrix4x4.identity;
				HandleUtility.AddControl(id, HandleUtility.DistanceToCircle(pointerScreenPosition, handleSize / 2f));
				Handles.matrix = cachedMatrix;
				break;
			}
			
			return returnValue;

		}

#if UNITY_5_6_OR_NEWER
        public static float TextureRotationHandle(Vector3 position, Quaternion baseOrientation, Vector3 normal, Vector3 nullForward, float currentRotation, float handleSize, Handles.CapFunction capfunc) {
#else
        public static float TextureRotationHandle(Vector3 position, Quaternion baseOrientation, Vector3 normal, Vector3 nullForward, float currentRotation, float handleSize, Handles.DrawCapFunction capfunc) {
#endif

			int id = GUIUtility.GetControlID(TexturePositionHandleXHash, FocusType.Passive);
			lastDragHandleId = id;
//			Vector3 startScreenPosition = Handles.matrix.MultiplyPoint(position);
			Vector3 pointerScreenPosition = Handles.matrix.MultiplyPoint(position + (baseOrientation * Vector3.forward * handleSize * 10));
			//			Vector3 screenDirectionVector = (pointerScreenPosition - startScreenPosition);
			Matrix4x4 cachedMatrix = Handles.matrix;
			
			float returnValue = 0f;
			normal.Normalize();
			nullForward.Normalize();

			switch (Event.current.GetTypeForControl(id)) {
			case EventType.KeyDown:
				if (!holdingCtrl) {
					if (Event.current.keyCode == KeyCode.LeftControl) {
						rotationStepStartPoint = currentRotation;
						holdingCtrl = true;
					}
					else {
						holdingCtrl = false;
						rotationStepStartPoint = 0f;
					}
				}
				break;
			case EventType.KeyUp:
				holdingCtrl = false;
				rotationStepStartPoint = 0f;
				break;
			case EventType.MouseDown:
				if (HandleUtility.nearestControl == id && Event.current.button == 0) {
					GUIUtility.hotControl = id;
					dragHandleMousePos = Event.current.mousePosition;
					Event.current.Use();
					EditorGUIUtility.SetWantsMouseJumping(1);
					
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == id && Event.current.button == 0) {
					GUIUtility.hotControl = 0;
					Event.current.Use ();
					EditorGUIUtility.SetWantsMouseJumping(0);
				}
				break;
			case EventType.MouseDrag:
				if (GUIUtility.hotControl == id) {
//					ATTHandles.currentRotation = currentRotation;
					Vector2 newMousePos = Event.current.mousePosition;
					newMousePos.y = TextureTilingEditorWindow.sceneCamera.pixelHeight - Event.current.mousePosition.y;
//					Debug.Log ("newMousePos = " + newMousePos);
//					Debug.Log ("Raycast Plane: " + raycastPlane..ToString());
//					Plane raycastPlane = new Plane(normal, position);
					float distance;
					Ray rayCastRay = TextureTilingEditorWindow.sceneCamera.ScreenPointToRay(newMousePos);
					if (new Plane(normal, position).Raycast(rayCastRay, out distance)) {
						positionInWorld = rayCastRay.GetPoint(distance); //TextureTilingEditorWindow.sceneCamera.ScreenToWorldPoint(new Vector3(newMousePos.x, newMousePos.y, distance));
//						positionInWorld.y = (position + nullForward).y;
						Vector3 referenceRight = Vector3.Cross(normal, nullForward).normalized;
						Vector3 currentRotationVector = (positionInWorld - position).normalized;
//						float angle = Vector3.Angle(nullForward, currentRotationVector);
//						float sign = 0f;
//						float dot = Vector3.Dot(referenceRight, currentRotationVector);
//						if (dot > 0) {
//							sign = -1f;
//						}
//						else {
//							sign = 1f;
//						}
						returnValue = -1f * Mathf.Sign(Vector3.Dot(referenceRight, currentRotationVector)) * Vector3.Angle(nullForward, currentRotationVector);
						if (holdingCtrl) {
							float diff = returnValue - rotationStepStartPoint;
							while (diff < 0f) {
								diff += 360f;
							}
							while (diff > 180f) {
								diff -= 360f;
							} 
							Debug.Log ("Raw diff: " + diff);
							if (diff >= 180f) {
								if (rotationStepStartPoint < 180f) {
									diff = (returnValue - 360f) - rotationStepStartPoint;
								}
								else if (rotationStepStartPoint > 180f) {
									diff = returnValue - (rotationStepStartPoint - 360f);
								}
							}
							Debug.Log ("Modified diff: " + diff);
							if (Mathf.Abs(diff) >= TextureTilingEditorWindow.rotationStepSize) {
								if (diff > 0) {
									returnValue = rotationStepStartPoint + TextureTilingEditorWindow.rotationStepSize;
									while (returnValue >= 360f) {
										returnValue -= 360f;
									}
									rotationStepStartPoint = returnValue;
								}
								else if (diff < 0) {
									returnValue = rotationStepStartPoint - TextureTilingEditorWindow.rotationStepSize;
									while (returnValue < 0f) {
										returnValue += 360f;
									}
									rotationStepStartPoint = returnValue;
								}
							}
							else {
								returnValue = rotationStepStartPoint;
							}
							Debug.Log ("rotationStepStartPoint: " + rotationStepStartPoint);
							Debug.Log ("returnValue: " + returnValue);
						}
//						Debug.Log("Raw Agnle: " + angle);
//						Debug.Log("sign: " + sign);
//						Debug.Log("New Angle: " + returnValue);
					}
					else {
						Debug.LogError("How can you NOT hit a plane??");
					}
//					Debug.Log ("positionInWorld = " + positionInWorld);
//					float angle = Vector3.Angle(nullForward, positionInWorld);
//					Debug.Log ("angle = " + angle);
//					Vector2 dragDelta = newMousePos - dragHandleMousePos;
//					dragDelta.y *= -1f;
//					Vector2 screenDirectionVector = Camera.current.WorldToScreenPoint(pointerScreenPosition) - Camera.current.WorldToScreenPoint(startScreenPosition);
//					if ((screenDirectionVector + dragDelta).sqrMagnitude > screenDirectionVector.sqrMagnitude) { 
//						returnValue += (newMousePos - dragHandleMousePos).magnitude / 500f;
//					}
//					else {
//						returnValue += (newMousePos - dragHandleMousePos).magnitude / (-500f);
//					}
//					returnValue = angle;

					dragHandleMousePos = newMousePos;

					GUI.changed = true;
					Event.current.Use();
				}
				break;
			case EventType.Repaint:
				Color currentColor = Handles.color;
				if (GUIUtility.hotControl == id) {
					Handles.color = Handles.selectedColor;
				}
				Handles.matrix = Matrix4x4.identity;
				Vector3 handlePosition = position + (baseOrientation * Vector3.forward * handleSize * 10);
				Handles.DrawLine(position, handlePosition);
#if UNITY_5_6_OR_NEWER
				capfunc(id, handlePosition, baseOrientation, handleSize * 2, EventType.Repaint);
#else
                capfunc(id, handlePosition, baseOrientation, handleSize * 2);
#endif
                Handles.matrix = cachedMatrix;
				Handles.color = currentColor;
				break;
			case EventType.Layout:
				Handles.matrix = Matrix4x4.identity;
				HandleUtility.AddControl(id, HandleUtility.DistanceToCircle(pointerScreenPosition, 5 * handleSize));
				Handles.matrix = cachedMatrix;
				break;
			}
			
			return returnValue;

		}

#if UNITY_5_6_OR_NEWER
        public static float TextureScaleHandle(Vector3 position, Quaternion rotation, float handleSize, Handles.CapFunction capfunc) {
#else
        public static float TextureScaleHandle(Vector3 position, Quaternion rotation, float handleSize, Handles.DrawCapFunction capfunc) {
#endif

			int id = GUIUtility.GetControlID(TexturePositionHandleXHash, FocusType.Passive);
			lastDragHandleId = id;
			Vector3 startScreenPosition = Handles.matrix.MultiplyPoint(position);
			Vector3 pointerScreenPosition = position + (rotation * Vector3.forward * handleSize * 10);
//			Vector3 pointerScreenPosition = Handles.matrix.MultiplyPoint(position + (rotation * Vector3.forward * handleSize * 10));
			//			Vector3 screenDirectionVector = (pointerScreenPosition - startScreenPosition);
			Matrix4x4 cachedMatrix = Handles.matrix;
			
			float returnValue = 0f;
			switch (Event.current.GetTypeForControl(id)) {
			case EventType.KeyDown:
				if (!holdingCtrl) {
					if (Event.current.keyCode == KeyCode.LeftControl) {
						holdingCtrl = true;
						stepBuffer = 0f;
					}
					else {
						holdingCtrl = false;
						stepBuffer = 0f;
					}
				}
				break;
			case EventType.KeyUp:
				holdingCtrl = false;
				stepBuffer = 0f;
				break;
			case EventType.MouseDown:
				if (HandleUtility.nearestControl == id && Event.current.button == 0) {
					GUIUtility.hotControl = id;
					dragHandleMousePos = Event.current.mousePosition;
//					dragHandleMousePos.y = TextureTilingEditorWindow.sceneCamera.pixelHeight - Event.current.mousePosition.y;
					//Debug.Log ("mouse down dragHandleMousePos: " + dragHandleMousePos);
					Event.current.Use();
					EditorGUIUtility.SetWantsMouseJumping(1);
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == id && Event.current.button == 0) {
					GUIUtility.hotControl = 0;
					dragHandleMousePos = Event.current.mousePosition;
//					dragHandleMousePos.y = TextureTilingEditorWindow.sceneCamera.pixelHeight - Event.current.mousePosition.y;
					//Debug.Log ("mouse up dragHandleMousePos: " + dragHandleMousePos);
					Event.current.Use ();
					EditorGUIUtility.SetWantsMouseJumping(0);
				}
				break;
			case EventType.MouseDrag:
				if (GUIUtility.hotControl == id) {
					Vector2 newMousePos = Event.current.mousePosition;
//					newMousePos.y = TextureTilingEditorWindow.sceneCamera.pixelHeight - Event.current.mousePosition.y;
					Vector2 dragDelta = newMousePos - dragHandleMousePos;
					dragDelta.y *= -1f;
					Vector2 screenDirectionVector = Camera.current.WorldToScreenPoint(pointerScreenPosition) - Camera.current.WorldToScreenPoint(startScreenPosition);
					if ((screenDirectionVector + dragDelta).sqrMagnitude > screenDirectionVector.sqrMagnitude) { 
						returnValue = (newMousePos - dragHandleMousePos).magnitude / 100f;
					}
					else {
						returnValue = (newMousePos - dragHandleMousePos).magnitude / (-100f);
					}
					if (holdingCtrl) {
						stepBuffer += returnValue;
						if (Mathf.Abs(stepBuffer) >= TextureTilingEditorWindow.scaleStepSize) {
							returnValue = TextureTilingEditorWindow.scaleStepSize * Mathf.Sign(stepBuffer);
							stepBuffer = 0f;
						}
						else {
							returnValue = 0f;
						}
					}
//					Debug.Log ("old dragHandleMousePos: " + dragHandleMousePos);
//					Debug.Log ("Scale Delta: " + returnValue);
					dragHandleMousePos = newMousePos;
//					Debug.Log ("new dragHandleMousePos: " + dragHandleMousePos);
					
					GUI.changed = true;
					Event.current.Use();
				}
				break;
			case EventType.Repaint:
				Color currentColor = Handles.color;
				if (GUIUtility.hotControl == id) {
					Handles.color = Handles.selectedColor;
				}
				Handles.matrix = Matrix4x4.identity;
				Handles.DrawLine(position, pointerScreenPosition);
#if UNITY_5_6_OR_NEWER
				capfunc(id, pointerScreenPosition, rotation, handleSize * 1.25f, EventType.Repaint);
#else
                capfunc(id, pointerScreenPosition, rotation, handleSize * 1.25f);
#endif
                Handles.matrix = cachedMatrix;
				Handles.color = currentColor;
				break;
			case EventType.Layout:
				Handles.matrix = Matrix4x4.identity;
				HandleUtility.AddControl(id, HandleUtility.DistanceToCircle(pointerScreenPosition, 5 * handleSize));
				Handles.matrix = cachedMatrix;
				break;
			}
			return returnValue;
		}

#if UNITY_5_6_OR_NEWER
        public static bool TextureAlignHandle(Vector3 position, Quaternion rotation, float handleSize, Handles.CapFunction capfunc) {
#else
		public static bool TextureAlignHandle(Vector3 position, Quaternion rotation, float handleSize, Handles.DrawCapFunction capfunc) {
#endif

			int id = GUIUtility.GetControlID(TexturePositionHandleXHash, FocusType.Passive);
			lastDragHandleId = id;
			Vector3 screenPosition = Handles.matrix.MultiplyPoint(position);
			Matrix4x4 cachedMatrix = Handles.matrix;
			
			bool returnValue = false;
			switch (Event.current.GetTypeForControl(id)) {
			case EventType.MouseDown:
				if (HandleUtility.nearestControl == id && Event.current.button == 0) {
					GUIUtility.hotControl = id;
					mouseDown = true;
					Event.current.Use();
					EditorGUIUtility.SetWantsMouseJumping(1);
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == id) {
					GUIUtility.hotControl = 0;
					if (mouseDown) {
						returnValue = Event.current.button == 0;
					}
					Event.current.Use ();
					EditorGUIUtility.SetWantsMouseJumping(0);
				}
				break;
//			case EventType.MouseDrag:
//				break;
			case EventType.Repaint:
				Color currentColor = Handles.color;
				if (GUIUtility.hotControl == id) {
					Handles.color = Handles.selectedColor;
				}
				Handles.matrix = Matrix4x4.identity;
#if UNITY_5_6_OR_NEWER
				capfunc(id, screenPosition, rotation, handleSize * 1.25f, EventType.Repaint);
#else
                capfunc(id, screenPosition, rotation, handleSize * 1.25f);
#endif
                Handles.matrix = cachedMatrix;
				Handles.color = currentColor;
				break;
			case EventType.Layout:
				Handles.matrix = Matrix4x4.identity;
				HandleUtility.AddControl(id, HandleUtility.DistanceToCircle(screenPosition, handleSize));
				Handles.matrix = cachedMatrix;
				break;

			}
			return returnValue;

		}

	}

}