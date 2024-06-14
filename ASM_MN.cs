using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Threading;
using System;
using System.Text;

public class ASM_MN : Singleton<ASM_MN>
{
    public List<Region> listRegion = new List<Region>();
    public List<Players> listPlayer = new List<Players>();
    private int currentPlayerId;

    protected override void Awake()
    {
        base.Awake();
        createRegion();
    }

    private void createRegion()
    {
        listRegion.Add(new Region(0, "VN"));
        listRegion.Add(new Region(1, "VN1"));
        listRegion.Add(new Region(2, "VN2"));
        listRegion.Add(new Region(3, "JS"));
        listRegion.Add(new Region(4, "VS"));
    }

    public string CalculateRank(int score)
    {
        // sinh viên viết tiếp code ở đây
        if (score < 100)
        {
            return "Hạng Đồng";
        }
        else if (score < 500)
        {
            return "Bạc";
        }
        else if (score < 1000)
        {
            return "Vàng";
        }
        else
        {
            return "Kim Cương";
        }
    }

    public void YC1()
    {
        // sinh viên viết tiếp code ở đây
        string name = ScoreKeeper.Instance.GetUserName();
        int id = ScoreKeeper.Instance.GetID();
        int score = ScoreKeeper.Instance.GetScore();
        int idR = ScoreKeeper.Instance.GetIDregion();
        string regionName = "VN1";
        if (idR == 0)
        {
            regionName = name;
        }
        else if (idR == 1)
        {
            regionName = name;
        }

        Players player = new Players(1, "Linh", score = 60, new Region(1, "VN1"))
        {
            Rank = CalculateRank(score)
        };
        listPlayer.Add(player);
        Players player2 = new Players(2, "Nhím", score = 300, new Region(2, "VN2"))
        {
            Rank = CalculateRank(score)
        };
        listPlayer.Add(player2);
        Players player3 = new Players(3, "Hải Saki", score = 1000, new Region(2, "VS"))
        {
            Rank = CalculateRank(score)
        };
        listPlayer.Add(player3);
        Players player4 = new Players(4, "Mixi", score = 0, new Region(4, "JS"))
        {
            Rank = CalculateRank(score)
        };
        listPlayer.Add(player4);
        Region playerRegion1 = new Region(idR, regionName);
        Players player5 = new Players(5, "Anh", score = 600, playerRegion1)
        {
            Rank = CalculateRank(score)
        };
        listPlayer.Add(player4);
    }
    public void YC2()
    {
        // sinh viên viết tiếp code ở đây
        foreach (Players player in listPlayer)
        {
            Debug.Log($"Name: {player.Name}, Score: {player.Score}, Region: {player.PlayerRegion.Name}");
        }
    }
    public void YC3()
    {
        // sinh viên viết tiếp code ở đây
        int currentPlayerScore = 250;
        var lowerScorePlayers = listPlayer.Where(p => p.Score < currentPlayerScore);
        foreach (var player in lowerScorePlayers)
        {
            Debug.Log($"Id: {player.Id}, Name: {player.Name}, Score: {player.Score}, Region: {player.PlayerRegion}, Rank: {player.Rank}");
        }
    }


    public void YC4()
    {
        // sinh viên viết tiếp code ở đây
        var player = listPlayer.FirstOrDefault(p => p.Id == currentPlayerId);
        if (player != null)
        {
            Debug.Log($"Id: {player.Id}, Name: {player.Name}, Score: {player.Score}, Region: {player.PlayerRegion}, Rank: {player.Rank}");
        }
    }


    public void YC5()
    {
        // sinh viên viết tiếp code ở đây
        var sortedPlayers = listPlayer.OrderByDescending(p => p.Score);
        foreach (var player in sortedPlayers)
        {
            Debug.Log($"Id: {player.Id}, Name: {player.Name}, Score: {player.Score}, Region: {player.PlayerRegion}, Rank: {player.Rank}");
        }
    }


    public void YC6()
    {
        // sinh viên viết tiếp code ở đây
        var lowestScorePlayers = listPlayer.OrderBy(p => p.Score).Take(5);
        foreach (var player in lowestScorePlayers)
        {
            Debug.Log($"Id: {player.Id}, Name: {player.Name}, Score: {player.Score}, Region: {player.PlayerRegion}, Rank: {player.Rank}");
        }
    }
    public void YC7()
    {
        Thread thread = new Thread(CalculateAndSaveAverageScoreByRegion);
        thread.Name = "BXH";
        thread.Start();
    }

    private void CalculateAndSaveAverageScoreByRegion()
    {
        List<Players> playersCopy;

        lock (listPlayer)
        {
            playersCopy = listPlayer.ToList();
        }

        var averageScoresByRegion = playersCopy
            .GroupBy(player => player.PlayerRegion.Name)
            .Select(group => new
            {
                Region = group.Key,
                AverageScore = group.Average(player => player.Score)
            }).ToList();

        string filePath = Path.Combine(Application.persistentDataPath, "bxhRegion.txt");
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var item in averageScoresByRegion)
            {
                writer.WriteLine($"Region: {item.Region}, Điểm số Trung bình: {item.AverageScore}");
            }
        }
        Debug.Log("Điểm số trung bình theo vùng đã được lưu vào bxhRegion.txt.");
    }
}

[SerializeField]
public class Region
{
    public int ID { get; set; }
    public string Name { get; set; }

    public Region(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}

[SerializeField]
public class Players
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public Region PlayerRegion { get; set; }
    public string Rank { get; set; }

    public Players(int id, string name, int score, Region region)
    {
        Id = id;
        Name = name;
        Score = score;
        PlayerRegion = region;
    }
}
