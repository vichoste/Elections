using org.apache.zookeeper;

namespace Elections;

internal class ZooKeeperWatcher : Watcher {
	public string Name { get; private set; }
	public ZooKeeperWatcher(string name) => this.Name = name;
	public override Task process(WatchedEvent @event) => Task.FromResult(0);
}