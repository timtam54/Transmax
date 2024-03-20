using vscode.Models;

namespace vscode.Data
{
    public interface iTrafficRepo
    {
        string? AddSample(TrafficSample ts);
        TrafficSample? GetSample(string id);

        List<TrafficSample>? GetLastForEachBot();
        
    }
}