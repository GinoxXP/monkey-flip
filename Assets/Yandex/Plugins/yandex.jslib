mergeInto(LibraryManager.library, {

  AuthExternal: function () {
    auth();
  },

  SaveDataExternal: function (data) {
    var dataString = UTF8ToString(data);
    var obj = JSON.parse(dataString);
    player.setData(obj);
  },

  LoadDataExternal: function (){
    player.getData().then(_data => {
      const json = JSON.stringify(_data);
      gameInstance.SendMessage("Yandex", "LoadDataInternal", json);
    });
  }

  GetPlayerDataExternal: function () {
    gameInstance.SendMessage("Yandex", "SetPlayerNameInternal", player.getName());
    gameInstance.SendMessage("Yandex", "SetPlayerPhotoInternal", player.getPhoto());
  },

});
