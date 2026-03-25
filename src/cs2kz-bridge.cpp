#include "cs2kz-bridge.h"
#include "details.h"
#include <libmodule/module.h>

CKZBridgePlugin g_KZBridgePlugin;

PLUGIN_EXPOSE(CKZBridgePlugin, g_KZBridgePlugin);

bool (*pKZTimerService__RegisterEventListener)(KZTimerServiceEventListener* eventListener) = nullptr;
libmodule::CModule g_KZModule;
CKZBridgeDetails g_KZBridgeDetails;

CKZBridgePlugin* KZBridgePlugin() {
	return &g_KZBridgePlugin;
}

bool CKZBridgePlugin::Load(PluginId id, ISmmAPI* ismm, char* error, size_t maxlen, bool late) {
	PLUGIN_SAVEVARS();

	return true;
}

void CKZBridgePlugin::AllPluginsLoaded() {
	g_KZModule.InitFromName("cs2kz");
	if (!g_KZModule.IsValid()) {
#ifdef _OD
		DebuggerBreak();
#endif
		return;
	}

	pKZTimerService__RegisterEventListener = g_KZModule.FindPattern("48 89 4C 24 ? 48 83 EC ? 44 8B 0D ? ? ? ? 33 C0 45 85 C9 7E ? 48 8B 15").RCast<decltype(pKZTimerService__RegisterEventListener)>();
	pKZTimerService__RegisterEventListener(&g_KZBridgeDetails);
}

const char* CKZBridgePlugin::GetAuthor() {
	return "Nyano1337";
}

const char* CKZBridgePlugin::GetName() {
	return "CS2KZ External Bridge";
}

const char* CKZBridgePlugin::GetDescription() {
	return "Build api for other languages on top of cs2kz";
}

const char* CKZBridgePlugin::GetURL() {
	return "NULL";
}

const char* CKZBridgePlugin::GetLicense() {
	return "MIT License";
}

const char* CKZBridgePlugin::GetVersion() {
	return "dev";
}

const char* CKZBridgePlugin::GetDate() {
	return "1337";
}

const char* CKZBridgePlugin::GetLogTag() {
	return nullptr;
}
