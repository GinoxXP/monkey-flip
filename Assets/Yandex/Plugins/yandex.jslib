mergeInto(LibraryManager.library, {

  AuthExternal: function () {
    auth();
    gameInstance.SendMessage("Yandex", "UserAuthorizationCompleated");
  },

  GetPlayerDataExternal: function () {
    if(initPlayer){
      var data = {"id" : player.getID(), "name" : player.getName(), "avatarUrlSmall" : player.getPhoto('small'), "avatarUrlMedium" : player.getPhoto('medium'), "avatarUrlLarge" : player.getPhoto('large')};
      console.log("Player data is get");
      gameInstance.SendMessage("Yandex", "SetPlayerDataInternal", JSON.stringify(data));
    }
    else {
      console.log("Player data isn't get");
    }
  },

  ShowFullscreenAdvExternal: function () {
    ysdk.adv.showFullscreenAdv({
      callbacks: {
        onClose: function(wasShown) {
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
      }
    });
  },

  GetLeaderboardExternal: function () {
    ysdk.getLeaderboards()
      .then(lb => {
        // Получение 10 топов
        lb.getLeaderboardEntries("scoreLeaderboard", { quantityTop: 10 })
          .then(res => {
            console.log(res);
            gameInstance.SendMessage("Yandex", "SetLeaderboardInternal", JSON.stringify(res));
          });
      });
  },

});
