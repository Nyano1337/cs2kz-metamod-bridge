#include "details.h"
#include <kz/mode/kz_mode.h>

ScriptingEventTable* g_pScriptingEventTable;

bool RegisterScriptingEventTable(ScriptingEventTable* pScriptingEventTable) {
	if (!CKZBridgeDetails::m_bInjected) {
		return false;
	}

	if (pScriptingEventTable) {
		g_pScriptingEventTable = pScriptingEventTable;
	}

	return true;
}

void CKZBridgeDetails::OnTimerStartPost(KZPlayer* player, u32 courseGUID) {
	if (g_pScriptingEventTable) {
		g_pScriptingEventTable->OnTimerStartPost(player->GetController(), player->modeService->GetModeShortName(), courseGUID);
	}
}

void CKZBridgeDetails::OnTimerEndPost(KZPlayer* player, u32 courseGUID, f32 time, u32 teleportsUsed) {
	if (g_pScriptingEventTable) {
		g_pScriptingEventTable->OnTimerEndPost(player->GetController(), player->modeService->GetModeShortName(), courseGUID, time, teleportsUsed);
	}
}
