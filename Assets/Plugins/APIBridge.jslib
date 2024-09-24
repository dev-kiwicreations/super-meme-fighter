mergeInto(LibraryManager.library, {

  InitWalletID: function () {
    
    var walletID = "myWalletID"; 
    SendMessage('API', 'onWalletIDRecieved', walletID);

  },
});