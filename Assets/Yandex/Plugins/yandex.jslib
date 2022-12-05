mergeInto(LibraryManager.library, {

  GetPlayerData: function () {
    gameInstance.SendMessage("Yandex", "SetPlayerName", player.getName());
    gameInstance.SendMessage("Yandex", "SetPlayerPhoto", player.getPhoto());
  },

});
