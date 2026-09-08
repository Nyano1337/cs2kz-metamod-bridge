#include "details.h"
#include "cs2kz-bridge.h"

ScriptingEventTable* g_pScriptingEventTable;

bool RegisterScriptingEventTable(ScriptingEventTable* pScriptingEventTable) {
	if (pScriptingEventTable) {
		g_pScriptingEventTable = pScriptingEventTable;
	}

	return true;
}

void CKZBridgeDetails::OnTimerStartPost(int slot, const KZCourseInfo& course) {
	if (g_pScriptingEventTable) {
		KZTimerStatus state {};
		if (GetCS2KZ()->GetTimerStatus(slot, &state)) {
			g_pScriptingEventTable->OnTimerStartPost(slot, state.modeShortName, course.name);
		}
	}
}

void CKZBridgeDetails::OnTimerEndPost(int slot, const KZCourseInfo& course, float time, uint32_t teleportsUsed) {
	if (g_pScriptingEventTable) {
		KZTimerStatus state {};
		if (GetCS2KZ()->GetTimerStatus(slot, &state)) {
			g_pScriptingEventTable->OnTimerEndPost(slot, state.modeShortName, course.name, time, teleportsUsed);
		}
	}
}
