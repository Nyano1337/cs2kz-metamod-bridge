#pragma once

#include <kz/timer/kz_timer.h>

class CKZBridgeDetails : public KZTimerServiceEventListener {
public:
	virtual void OnTimerStartPost(KZPlayer* player, u32 courseGUID) override;
	virtual void OnTimerEndPost(KZPlayer* player, u32 courseGUID, f32 time, u32 teleportsUsed) override;

public:
	static inline bool m_bInjected = false;
};

struct ScriptingEventTable {
	void (*OnTimerStartPost)(void* pPlayerController, const char* pszMode, u32 courseGUID);
	void (*OnTimerEndPost)(void* pPlayerController, const char* pszMode, u32 courseGUID, f32 time, u32 teleportsUsed);
};

DLL_EXPORT bool RegisterScriptingEventTable(ScriptingEventTable* pScriptingEventTable);
