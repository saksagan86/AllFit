import React from 'react';
import './App.css';
import { Routes, Route } from 'react-router-dom';


import InfoBlock from './assets/infoBlock.jsx'
import HomePage from './HomePage.jsx'
import AanbodOverzicht from './Components/AanbodOverzicht';
import DetailPagina from './Pages/DetailPagina'; 

function App() {
    return (
        <div>
            <link href="https://fonts.googleapis.com/css2?family=Noto+Sans&family=Roboto:wght@700&display=swap" rel="stylesheet"></link>
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/aanbod" element={<AanbodOverzicht />} />
                <Route path="/aanbod/:sportNaam" element={<DetailPagina />} />
            </Routes>
        </div>
    );
}

export default App;