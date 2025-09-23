import React from 'react';
import './App.css';
import {Outlet} from "react-router";



function App() {
  
  return (
    <div>
      <p>hi!</p>
      <Outlet />
    </div>
  );
}

export default App;
