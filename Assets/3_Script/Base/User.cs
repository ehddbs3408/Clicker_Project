using System.Collections.Generic;
[System.Serializable]
public class User
{
    public long love;
    public List<Stat> statList = new List<Stat>();
    public List<Event> evevntList = new List<Event>();
}