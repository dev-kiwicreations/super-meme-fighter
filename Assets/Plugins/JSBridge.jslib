mergeInto(LibraryManager.library, {
    GetGameMode: function () {
        if (typeof GameMode !== 'undefined' && typeof P1 !== 'undefined' && typeof P2 !== 'undefined' && typeof StageIndex !== 'undefined') {
            var gameModeData = {
                gameMode: GameMode,
                player1: P1,
                player2: P2,
                stageIndex: StageIndex
            };
            console.log("GameMode is Switched:", GameMode);
            return JSON.stringify(gameModeData);
        } else {
            return null;
        }
    }
});
