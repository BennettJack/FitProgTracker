import React from 'react';
import logo from './logo.svg';
import './App.css';
import axios from "axios";
import { useEffect, useState } from "react";
import LoginSignup from "./Pages/Account/LoginSignup";



interface CheckCookieResponse {
  Authenticated: boolean;
  Username?: string;
  error?: string;
}
function App() {


  const [status, setStatus] = useState<string>("");

  const checkAuth = async () => {
    try {
      const response = await axios.get("https://localhost:7206/UserAccount/testAuth", {
        withCredentials: true, // critical: sends the authentication cookie
      });
      if (response.status === 200) {
        setStatus("Authenticated ✅");
      } else {
        setStatus(`Unexpected response: ${response.status}`);
      }
    } catch (error: any) {
      if (error.response?.status === 401) {
        setStatus("Not Authenticated ❌");
      } else {
        setStatus(`Error: ${error.message}`);
      }
    }
  };
  
  
  function handleTest(){
    
  }
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
          <button onClick={checkAuth}>test</button>
        </p>
        <LoginSignup/>
        <p>{status}</p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
