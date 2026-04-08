import React from 'react';
import './App.css';
import { Routes, Route } from 'react-router-dom';

import Navbar from './components/Navbar.jsx'
import HomePage from './pages/HomePage.jsx'
import ContactPage from './pages/ContactPage.jsx'
import LocatiesPage from './pages/LocatiesPage.jsx'
import NotFoundPage from './pages/NotFoundPage.jsx'
import AanbodOverzicht from './Components/AanbodOverzicht';
import DetailPagina from './Pages/DetailPagina'; 

function App() {
    return (
        <div>
            <link href="https://fonts.googleapis.com/css2?family=Noto+Sans&family=Roboto:wght@700&display=swap" rel="stylesheet"></link>
            <Navbar></Navbar>
            <Routes>
                <Route path="/" element={<HomePage />} />
                    <Route path="/locaties" element={<LocatiesPage />} />
                <Route path="/contact" element={<ContactPage />} />
                <Route path="/aanbod" element={<AanbodOverzicht />} />
                    <Route path="*" element={<NotFoundPage />} />
                <Route path="/aanbod/:sportNaam" element={<DetailPagina />} />
                <></>
            </Routes>
        </div>
    );
}

export default App;
