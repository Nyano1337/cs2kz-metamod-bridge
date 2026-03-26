using SwiftlyS2.Shared.Plugins;
using SwiftlyS2.Shared;
using CS2KZEvents;
using CS2KZEvents.src.Events;

namespace Test;

[PluginMetadata(Id = "Test", Version = "1.0.0", Name = "Test", Author = "Nyano1337", Description = "No description.")]
public partial class Test : BasePlugin
{
	private IKZEventListener _kzEventListener;
	public Test(ISwiftlyCore core) : base(core)
	{
	}

	public override void ConfigureSharedInterface(IInterfaceManager interfaceManager)
	{
	}

	public override void UseSharedInterface(IInterfaceManager interfaceManager)
	{
		_kzEventListener = interfaceManager.GetSharedInterface<IKZEventListener>("KZEventListener");

		_kzEventListener.OnTimerStartPost += (e) =>
		{

		};

		_kzEventListener.OnTimerEndPost += (e) =>
		{

		};
	}

	public override void Load(bool hotReload)
	{

	}

	public override void Unload()
	{
	}
}
