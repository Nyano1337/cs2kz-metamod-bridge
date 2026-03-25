#include "details.h"
#include <libmodule/module.h>

bool (*pKZTimerService__RegisterEventListener)(KZTimerServiceEventListener* eventListener) = nullptr;
libmodule::CModule g_KZModule;
CKZBridgeDetails g_KZBridgeDetails;

void CKZBridgeDetails::OnTimerStartPost(KZPlayer* player, u32 courseGUID) {
	int a = 1;
}

void CKZBridgeDetails::OnTimerEndPost(KZPlayer* player, u32 courseGUID, f32 time, u32 teleportsUsed) {
	int a = 1;
}

void CKZBridgeDetails::OnTryLoadKZPlugin() {
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
