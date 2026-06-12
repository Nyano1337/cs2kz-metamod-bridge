#include "cs2kz-bridge.h"
#include "details.h"
#include <libmodule/module.h>

CKZBridgePlugin g_KZBridgePlugin;
CKZBridgeDetails g_KZBridgeDetails;

PLUGIN_EXPOSE(CKZBridgePlugin, g_KZBridgePlugin);

libmodule::CModule g_KZModule;
bool (*pKZTimerService__RegisterEventListener)(KZTimerServiceEventListener* eventListener) = nullptr;
KZCourseDescriptor* (*pKZ__course__GetCourseByGUID)(int guid) = nullptr;

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

	auto pKZRecordingService__Init = g_KZModule.FindPattern("48 83 EC ? 48 8D 0D ? ? ? ? E8 ? ? ? ? B9");
	pKZTimerService__RegisterEventListener = pKZRecordingService__Init.Offset(11).FollowNearCallSelf().RCast<decltype(pKZTimerService__RegisterEventListener)>();
	if (pKZTimerService__RegisterEventListener) {
		pKZTimerService__RegisterEventListener(&g_KZBridgeDetails);
	}

	pKZ__course__GetCourseByGUID = g_KZModule.FindPattern("44 8B 0D ? ? ? ? 45 85 C9 7E ? 4C 8B 15 ? ? ? ? 33 C0 66 66 66 0F 1F 84 00 ? ? ? ? 4C 63 C0 4B 8B 14 C2 39 8A C8 00 00 00").RCast<decltype(pKZ__course__GetCourseByGUID)>();

	if (pKZTimerService__RegisterEventListener && pKZ__course__GetCourseByGUID) {
		CKZBridgeDetails::m_bInjected = true;
	}
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

const KZCourseDescriptor* KZ::course::GetCourse(u32 guid) {
	return pKZ__course__GetCourseByGUID(guid);
}
