#pragma once

#include <public/ics2kz.h>
#include <public/tier0/platform.h>

class CKZBridgeDetails : public ICS2KZEventListener {
public:
	virtual void OnTimerStartPost(int slot, const KZCourseInfo& course) override;

	// The player finished a run. `time` is the final run time in seconds.
	virtual void OnTimerEndPost(int slot, const KZCourseInfo& course, float time, uint32_t teleportsUsed) override;
};

struct ScriptingEventTable {
	void (*OnTimerStartPost)(int slot, const char* pszMode, const char* pszCourse);
	void (*OnTimerEndPost)(int slot, const char* pszMode, const char* pszCourse, float time, uint32_t teleportsUsed);
};

DLL_EXPORT bool RegisterScriptingEventTable(ScriptingEventTable* pScriptingEventTable);
