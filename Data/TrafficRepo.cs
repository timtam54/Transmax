using System.Text.Json;
using Microsoft.AspNetCore.Connections;
using StackExchange.Redis;
using vscode.Models;

namespace vscode.Data
{
    public class TrafficRepo : iTrafficRepo
    {
         public static string AllEaglebotLatest="AllLatest";
        private IConnectionMultiplexer _redis;

        public TrafficRepo(IConnectionMultiplexer redis)
        {
            _redis=redis;
        }
        public string? AddSample(TrafficSample ts)
        {
           if (_redis==null)
           {
            Console.WriteLine("db not Initialised");
            return null;
           }
            var db = _redis.GetDatabase();
            var ser = JsonSerializer.Serialize(ts);
            var id=$"sample:{Guid.NewGuid()}";
            db.StringSet(id,ser);
            //now need to take a copy of latest for each eaglebot and store it in AllEaglebotLatest set
            
     //       ts.id=ts.EagleBotid;
            var serLatest = ser;
            
           // db.SetAdd(AllEaglebotLatest,serLatest);
           db.HashSet(AllEaglebotLatest,new HashEntry[]
           {new HashEntry(ts.EagleBotid,ser)});
            return id;
        }
       
        public List<TrafficSample>? GetLastForEachBot()
        {
            var db = _redis.GetDatabase();
            var AllLatest = db.HashGetAll(AllEaglebotLatest);//.SetMembers(AllEaglebotLatest);
            if (AllLatest.Length>0)
            {
                var latest = Array.ConvertAll(AllLatest, val=>JsonSerializer.Deserialize<TrafficSample>(val.Value!)).ToList();
                return latest.Where(i=>i!=null).ToList()!;
            }
            return null;
        }

        public TrafficSample? GetSample(string id)
        {if (_redis==null)
           {
            Console.WriteLine("db not initialised");
            return null;
           }
            var db = _redis.GetDatabase();;
            var ser=db.StringGet(id);
            if (!ser.HasValue)
                return null;
            return JsonSerializer.Deserialize<TrafficSample>(ser!);
        }
    }
}