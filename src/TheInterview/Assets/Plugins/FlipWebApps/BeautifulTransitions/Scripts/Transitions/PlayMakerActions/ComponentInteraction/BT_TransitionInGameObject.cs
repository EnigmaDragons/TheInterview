//----------------------------------------------
// Flip Web Apps: Game Framework
// Copyright � 2016 Flip Web Apps / Mark Hewitt
//
// Please direct any bugs/comments/suggestions to http://www.flipwebapps.com
// 
// The copyright owner grants to the end user a non-exclusive, worldwide, and perpetual license to this Asset
// to integrate only as incorporated and embedded components of electronic games and interactive media and 
// distribute such electronic game and interactive media. End user may modify Assets. End user may otherwise 
// not reproduce, distribute, sublicense, rent, lease or lend the Assets. It is emphasized that the end 
// user shall not be entitled to distribute or transfer in any way (including, without, limitation by way of 
// sublicense) the Assets in any other way than as integrated components of electronic games and interactive media. 

// The above copyright notice and this permission notice must not be removed from any files.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//----------------------------------------------

#if PLAYMAKER
using HutongGames.PlayMaker;

namespace BeautifulTransitions.Scripts.Transitions.PlayMakerActions.ComponentInteraction
{
    /// <summary>
    /// Find and transition in any transitions contained on or as a child of the specified gameobject. Depending on 
    /// the TransitionChildren and MustTriggerDirect configuration, any further children of these transitions 
    /// will also be triggered.

    /// </summary>
    [ActionCategory("Beautiful Transitions")]
    [Tooltip("Transition in components on the specified GameObject and any children (based upon component configuration).")]
    [Title("Transition In - GameObject")]
    public class BT_TransitionInGameObject : FsmStateAction
    {
        /// <summary>
        /// The target GameObject to act upon
        /// </summary>
        [Tooltip("The target GameObject to act upon.")]
        [RequiredField]
        public FsmOwnerDefault Target;

//#if UNITY_EDITOR
//        public override string AutoName()
//        {
//            return "My Custom Action Name";
//        }
//#endif

        public override void Reset()
        {
            base.Reset();
            Target = null;
        }

        public override void OnEnter()
        {
            PerformAction();
            Finish();
        }

        /// <summary>
        /// The actual method that does the work
        /// </summary>
        void PerformAction()
        {
            var gameObject = Fsm.GetOwnerDefaultTarget(Target);
            if (gameObject != null)
            {
                TransitionHelper.TransitionIn(gameObject);
            }
        }
    }
}
#endif