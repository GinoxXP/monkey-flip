mergeInto(LibraryManager.library, {

  AuthExternal: function () {
    auth();
    gameInstance.SendMessage("Yandex", "UserAuthorizationCompleated");
  },

  GetPlayerPhotoExternal: function () {
    gameInstance.SendMessage("Yandex", "SetPlayerPhotoInternal", player.getPhoto("large"));
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
    ysdk.getLeaderboards()
      .then(lb => lb.getLeaderboardPlayerEntry("scoreLeaderboard"))
      .then(res => {
          console.log(res);
          gameInstance.SendMessage("Yandex", "SetLeaderboardEntryInternal", JSON.stringify(res));
      })
      .catch(err => {
        if (err.code === 'LEADERBOARD_PLAYER_NOT_PRESENT') {
          // Срабатывает, если у игрока нет записи в лидерборде
        }
      });
  },

  SetToLeaderboardExternal: function (score) {
    ysdk.getLeaderboards()
    .then(lb => {
      lb.setLeaderboardScore('scoreLeaderboard', score);
    });
  },

});
