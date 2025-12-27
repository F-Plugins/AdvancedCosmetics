# AdvancedCosmetics
Allows your players to setup their cosmetics with commands.

## Features
- Get Unturned cosmetics for free
- Manage cosmetics using commands, names, and IDs
- Supports all cosmetics in the game

## Commands
- **/customcosmetic \<name or id\>** - Sets your custom cosmetic
- **/removecosmetics** - Removes all your custom cosmetics

## Permissions
```xml
<Permission Cooldown="0">customcosmetic</Permission>
<Permission Cooldown="0">removecosmetics</Permission>
```

## Translations
```xml
<?xml version="1.0" encoding="utf-8"?>
<Translations xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Translation Id="RemoveCosmetics:Fail" Value="You haven't set up any custom cosmetics yet" />
  <Translation Id="RemoveCosmetics:Success" Value="Successfully removed all your cosmetics. Reconnect to the server to see the changes" />
  <Translation Id="CustomCosmetic:Usage" Value="Correct command usage: /customcosmetics <cosmeticId> | /customcosmetics <cosmeticName> [--force (reconnects you to the server so the changes get applied)]" />
  <Translation Id="CustomCosmetic:NotFound" Value="Cosmetic with id or name {0} was not found" />
  <Translation Id="CustomCosmetic:Success" Value="Successfully added the cosmetic {0}" />
</Translations>
```