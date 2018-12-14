namespace Puzzles
{
    public struct TaskData
    {
        public ITask Task { get; }
        public string Data { get; }
        public string Answer{ get; }

        public TaskData(ITask task, string data, string answer)
        {
            Task = task;
            Data = data;
            Answer = answer;
        }
    }
}