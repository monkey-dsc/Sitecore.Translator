Sitecore.Translator
===================

SeitenKern Translator is a all-round translator for your website and an important  module for all .NET Sitecore developers.

Nearly all websites are multilingual and managing translations is always a hard step in development progress.

This module helps you to manage your translations with ease! It builds on the dictionary domain concept introduced in version 6.6 of the Sitecore ASP.NET Web Content Management System (CMS).

It's very easy to use! Within 5 Minutes you are ready to take the full advantages of the module, and managing translations is blown out of your mind ;)

The module requires a small set of templates: 
- dictionary folder (for structure only)
- dictionary domain (item-id with this template should take place in your site config)
- dictionary folder (the folder for the dictionary index)
- dictionary entry (at least the entry template) 

Keep in mind: The dictionary domain has a fall-back domain! If a key is not found in the current dictionary, the translator searches for the key in the fallback domain and returns the translation item if key is found. Else, the translator adds the key to the current dictionary.

If a key was loaded from the fall-back domain and you set "AddToAllDictionaries" sometime later, the translator is smart enough to recognize this! It adds the translation to the current dictionary and to all other configured dictionaries where not an item with this key exists!

Important note: Copy your Sitecore.Kernel.dll to the lib/Sitecore-Folder


What's new?
===================

Version 1.0.10.5963

An issue #1 occured on environments with a SwitchMasterToWeb.config. This issue was fixed.

For performance optimization the translation are now gonna be cached! So if you translate something and nothing happens, clear your caches first.
