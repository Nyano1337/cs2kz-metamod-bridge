#include "cs2kz-bridge.h"
#include "details.h"

CKZBridgePlugin g_KZBridgePlugin;
PLUGIN_EXPOSE(CKZBridgePlugin, g_KZBridgePlugin);

ICS2KZ* g_pCS2KZ;
CKZBridgeDetails g_KZBridgeDetails;

bool CKZBridgePlugin::Load(PluginId id, ISmmAPI* ismm, char* error, size_t maxlen, bool late) {
	PLUGIN_SAVEVARS();

	return true;
}

void CKZBridgePlugin::AllPluginsLoaded() {
	g_pCS2KZ = (ICS2KZ*)g_SMAPI->MetaFactory(CS2KZ_INTERFACE, nullptr, nullptr);
	if (!g_pCS2KZ) {
#ifdef _OD
		DebuggerBreak();
#endif
		return;
	}

	g_pCS2KZ->RegisterEventListener(&g_KZBridgeDetails);
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

CKZBridgePlugin* KZBridgePlugin() {
	return &g_KZBridgePlugin;
}

ICS2KZ* GetCS2KZ() {
	return g_pCS2KZ;
}
