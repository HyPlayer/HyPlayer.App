using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.App;
public class PlayList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public User User { get; set; }
    public List<Song> Songs { get; set; }
    public int Count { get => Songs.Count(); }
    public static PlayList CreateSample()
    {
        var id = new Random().Next(100);
        string des = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        return new PlayList 
        { 
            Id = id, 
            Name = $"PlayList{id}",
            Description = des,
            User = User.CreateSample(),
            Songs = Song.CreateSampleList()
        };   
    }
    public static List<PlayList> CreateSampleList()
    {
        var list = new List<PlayList>();
        for (int i = 0; i < 8; i++)
        {
            list.Add(CreateSample());
        }
        return list;
    }
}