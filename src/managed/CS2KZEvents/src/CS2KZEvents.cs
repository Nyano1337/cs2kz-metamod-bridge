using CS2KZEvents.Events;
using CS2KZEvents.Shared;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;

namespace CS2KZEvents;

[PluginMetadata(Id = "CS2KZEvents", Version = "1.0.0", Name = "CS2KZEvents", Author = "Nyano1337", Description = "cs2kz API for SwiftlyS2")]
internal partial class CS2KZEvents : BasePlugin
{
	private readonly ISwiftlyCore _core;
	private readonly KZEventPublisher _kzEventPublisher;

	public CS2KZEvents(ISwiftlyCore core) : base(core)
	{
		_core = core;
		_kzEventPublisher = new KZEventPublisher(_core);

		_core.Event.OnStartupServer += _kzEventPublisher.OnStartupServer;
	}

	public override void ConfigureSharedInterface(IInterfaceManager interfaceManager)
	{
		interfaceManager.AddSharedInterface<IKZEventListener, KZEventListener>("KZEventListener", new KZEventListener());
	}

	public override void Load(bool hotReload) { }

	public override void Unload()
	{
		_kzEventPublisher.Dispose();
	}
}
