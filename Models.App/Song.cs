using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.App;

public class Song
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string DurationString { get; set; }

    public static List<Song> CreateSampleList()
    {
        return new List<Song>
        {
            new Song { Id = 1,Name = "Song#1",Artist="Artist#1",Album = "Album#1",DurationString = "1:00"},
            new Song { Id = 1,Name = "Song#2",Artist="Artist#2",Album = "Album#2",DurationString = "1:00"},
            new Song { Id = 1,Name = "Song#3",Artist="Artist#3",Album = "Album#3",DurationString = "1:00"},
            new Song { Id = 1,Name = "Song#4",Artist="Artist#4",Album = "Album#4",DurationString = "1:00"},
            new Song { Id = 1,Name = "Song#5",Artist="Artist#5",Album = "Album#5",DurationString = "1:00"},
        };
    }
}

