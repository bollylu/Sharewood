window.CryptoInterop = {
  encryptData: function (plaintext, password) {
    try {
      const encrypted = CryptoJS.AES.encrypt(plaintext, password).toString();
      return encrypted;
    } catch (error) {
      console.error('Encryption error:', error);
      return null;
    }
  },
  
  decryptData: function (ciphertext, password) {
    try {
      const decrypted = CryptoJS.AES.decrypt(ciphertext, password).toString(CryptoJS.enc.Utf8);
      return decrypted;
    } catch (error) {
      console.error('Decryption error:', error);
      return null;
    }
  }
};