mergeInto(LibraryManager.library, {

    AdImage : function()
    {
        sdk.adv.showFullscreenAdv({
            callbacks:
            {
                onClose: function(wasShown) {
                    console.log("-------------- close ---------------------");
                    myGameInstance.SendMessage("Yandex", "AdImageClose");
                },
                onError: function(error) {
                    console.log("----------------- error --------------------");
                }
            }
        })
    },

    _AdVideo : function(value)
    {
        sdk.adv.showRewardedVideo({
            callbacks: {
                onOpen: () => {
                  console.log('Video ad open.');
                },
                onRewarded: () => {
                  console.log('Rewarded!');
                  myGameInstance.SendMessage("Yandex", "AdVideoRewarded", value);
                },
                onClose: () => {
                  console.log('Video ad closed.');
                  myGameInstance.SendMessage("Yandex", "AdVideoClose", value);
                }, 
                onError: (e) => {
                  console.log('Error while open video ad:', e);
                }
            }
        })
    },
});