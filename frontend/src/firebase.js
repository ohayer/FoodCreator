import { initializeApp } from "firebase/app";
import { getAuth } from "firebase/auth";


// Your web app's Firebase configuration
const firebaseConfig = {
    apiKey: "AIzaSyDvQ4Mw4j7Ex86f1KuuQlER1drXqiFd4G0",
    authDomain: "foodcreator-b07b3.firebaseapp.com",
    projectId: "foodcreator-b07b3",
    storageBucket: "foodcreator-b07b3.firebasestorage.app",
    messagingSenderId: "864161986674",
    appId: "1:864161986674:web:7cbc152f3a7519f2056aeb"
};


const app = initializeApp(firebaseConfig);
export const auth = getAuth(app);
