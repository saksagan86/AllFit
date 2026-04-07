import React from 'react';
import './App.css';
import { Routes, Route } from 'react-router-dom';


import InfoBlock from './assets/infoBlock.jsx'
import HomePage from './HomePage.jsx'
import Navbar from './components/Navbar.jsx'
import AanbodPage from './pages/AanbodPage.jsx'
import ContactPage from './pages/ContactPage.jsx'
import LocatiesPage from './pages/LocatiesPage.jsx'
import NotFoundPage from './pages/NotFoundPage.jsx'
import AanbodOverzicht from './Components/AanbodOverzicht';
import DetailPagina from './Pages/DetailPagina'; 

function App() {
    return (
        <div>
            <link href="https://fonts.googleapis.com/css2?family=Noto+Sans&family=Roboto:wght@700&display=swap" rel="stylesheet"></link>
            <
            <Navbar />
            Routes>
                <Route path="/" element={<HomePage />} />
                    <Route path="/aanbod" element={<AanbodPage />} />
                    <Route path="/locaties" element={<LocatiesPage />} />
                    <Route path="/contact" element={<ContactPage />} />
                    <Route path="*" element={<NotFoundPage />} />
                <Route path="/aanbod" element={<AanbodOverzicht />} />
                <Route path="/aanbod/:sportNaam" element={<DetailPagina />} />
            </Routes>
        </div>
    );
}

export default App;