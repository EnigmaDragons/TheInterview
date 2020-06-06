using System;
using System.Collections.Generic;

[Serializable]
public class AppState
{
    public bool AppInstalled = false;
    public int CurrentLevel = 1;
    public int Creds = -2000;
    public int CurrentLevelXp = 0;
    public int NextLevelXp = 250;
    public List<AppQuestInfo> ActiveQuests = new List<AppQuestInfo>();
}
