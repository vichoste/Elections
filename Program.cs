using org.apache.zookeeper;

namespace Elections;

internal class Program {
	private const int _timeout = 5000;
	private const string _host = "localhost:2181";
	private const string _namespace = "/election";
	private const string _subNamespace = "/c";
	public static async Task Main() {
		var zooKeeper = new ZooKeeper(_host, _timeout, new ZooKeeperWatcher(_namespace));
		var path = await zooKeeper.createAsync(_namespace + _subNamespace, Array.Empty<byte>(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL_SEQUENTIAL);
		Console.WriteLine($"Path: {path}");
		var children = await zooKeeper.getChildrenAsync(_namespace, false);
		var sorted = children.Children.ToList();
		sorted.Sort();
		Console.WriteLine("Children: ");
		foreach (var child in sorted) {
			Console.WriteLine(child);
		}
		var first = children.Children[0];
		var node = path.Replace("/election/", string.Empty);
		Console.WriteLine($"Comparison: {first} - {node}");
		if (first == node) {
			Console.WriteLine($"I'm the great leader. Bow to me you mortals.");
		} else {
			Console.WriteLine($"I'm not the leader. I will pay tributes to the dear leader.");
		}
		_ = Console.ReadLine();
		await zooKeeper.closeAsync();
	}
}