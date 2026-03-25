using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;

namespace CS2KZEvents.src;

[PluginMetadata(Id = "CS2KZEvents", Version = "1.0.0", Name = "CS2KZEvents", Author = "Nyano1337", Description = "cs2kz API for SwiftlyS2")]
public partial class CS2KZEvents : BasePlugin
{
	private readonly ISwiftlyCore _core;
	private readonly KZEventBus _kzEventBus;
	public CS2KZEvents(ISwiftlyCore core) : base(core)
	{
		_core = core;
		_kzEventBus = new KZEventBus();

		_core.Event.OnStartupServer += _kzEventBus.OnStartupServer;
	}

	public override void Load(bool hotReload) {}

	public override void Unload()
	{
		_kzEventBus.Dispose();
	}
}
