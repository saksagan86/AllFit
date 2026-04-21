import React from 'react';
import './App.css';
import { Routes, Route } from 'react-router-dom';

import Navbar from './Components/Navbar.jsx'
import HomePage from './Pages/HomePage.jsx'
import ContactPage from './Pages/ContactPage.jsx'
import LocatiesPage from './Pages/LocatiesPage.jsx'
import NotFoundPage from './Pages/NotFoundPage.jsx'
import AanbodOverzicht from './Components/AanbodOverzicht';
import DetailPagina from './Pages/DetailPagina'; 
import ScrollNaarBoven from './Components/ScrollNaarBoven';
import InschrijfPage from './Pages/InschrijfPage'; 

function App() {
    return (
        <div>
            <ScrollNaarBoven /> 
            <link href="https://fonts.googleapis.com/css2?family=Noto+Sans&family=Roboto:wght@700&display=swap" rel="stylesheet"></link>
            <Navbar></Navbar>
            <div className="page-content">
            <Routes>
                <Route path="/" element={<HomePage />} />
                    <Route path="/locaties" element={<LocatiesPage />} />
                <Route path="/contact" element={<ContactPage />} />
                <Route path="/aanbod" element={<AanbodOverzicht />} />
                    <Route path="*" element={<NotFoundPage />} />
                    <Route path="/aanbod/:sportNaam" element={<DetailPagina />} />
                    <Route path="/inschrijven" element={<InschrijfPage />} /> 
                <></>
                </Routes>
            </div>
        </div>
    );
}

export default App;
