#include "details.h"

ScriptingEventTable* g_pScriptingEventTable;

void RegisterScriptingEventTable(ScriptingEventTable* pScriptingEventTable) {
	if (pScriptingEventTable) {
		g_pScriptingEventTable = pScriptingEventTable;
	}
}

void CKZBridgeDetails::OnTimerStartPost(KZPlayer* player, u32 courseGUID) {
	if (g_pScriptingEventTable) {
		g_pScriptingEventTable->OnTimerStartPost(player->GetController(), courseGUID);
	}
}

void CKZBridgeDetails::OnTimerEndPost(KZPlayer* player, u32 courseGUID, f32 time, u32 teleportsUsed) {
	if (g_pScriptingEventTable) {
		g_pScriptingEventTable->OnTimerEndPost(player->GetController(), courseGUID, time, teleportsUsed);
	}
}
