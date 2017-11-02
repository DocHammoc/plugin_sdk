###########################################
aquasuite plugin sdk
Version 2.0 - 2017/11/02
Compatible with: aquasuite 2014.2
###########################################

General:
All demos are written with C# and compatible with x86 and x64

IDE:
Visual Studio 2015 - 2017 C#

Plugins preferences:
- .Net 4.6.1 Profile as Target
- dll must complied for platform "Any" (x86 and x64 compatible)

Import plugins:
Import sensor data from external sources.

Export pugins:
Used in automatic log data exports.
Live data from the aquasuite log module is transfered to the plugin.

Every plugin dll are located in:
[aquasuite installation path]\Plugins (C:\Program Files\aquasuite\Plugins)

Ressources:
Interface Lib to implement a Plugin, PluginCore.dll. 


DEMO projects:
###########################################

PluginExportSHM: 
Export plugin, data export to Shared Memory

PluginExportShmTest:
Test/Demo program for PluginExportSHM, read out the content from exported Data

PluginExportXML:
Export plugin, data export to XML file

PluginImportDemo:
Import plugin, simple demo for importing any data to the aquasuite

PluginImportOHM:
Import plugin, import Open Hardware Monitor data to the aquasuite