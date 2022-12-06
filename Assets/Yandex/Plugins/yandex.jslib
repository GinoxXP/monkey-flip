mergeInto(LibraryManager.library, {

  Auth: function () {
    auth();
  },

  GetPlayerData: function () {
    gameInstance.SendMessage("Yandex", "SetPlayerName", player.getName());
    gameInstance.SendMessage("Yandex", "SetPlayerPhoto", player.getPhoto());
  },

});
