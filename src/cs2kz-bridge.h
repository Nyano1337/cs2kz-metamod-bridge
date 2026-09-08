#pragma once

#include <ISmmPlugin.h>

class CKZBridgePlugin : public ISmmPlugin, public IMetamodListener {
private:
	virtual bool Load(PluginId id, ISmmAPI* ismm, char* error, size_t maxlen, bool late) override;
	virtual void AllPluginsLoaded() override;
	virtual const char* GetAuthor() override;
	virtual const char* GetName() override;
	virtual const char* GetDescription() override;
	virtual const char* GetURL() override;
	virtual const char* GetLicense() override;
	virtual const char* GetVersion() override;
	virtual const char* GetDate() override;
	virtual const char* GetLogTag() override;
};

extern CKZBridgePlugin* KZBridgePlugin();
extern class ICS2KZ* GetCS2KZ();
