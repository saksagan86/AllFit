import React from 'react';
import './App.css';
import { Routes, Route } from 'react-router-dom';

import Navbar from './Components/Navbar.jsx';
import ProtectedRoute from './Components/ProtectedRoute.jsx';
import ScrollNaarBoven from './Components/ScrollNaarBoven';

import InschrijfPage from './Pages/InschrijfPage';
import HomePage from './Pages/HomePage.jsx';
import ContactPage from './Pages/ContactPage.jsx';
import LocatiesPage from './Pages/LocatiesPage.jsx';
import NotFoundPage from './Pages/NotFoundPage.jsx';
import DetailPagina from './Pages/DetailPagina';
import LoginPage from './Pages/LoginPage.jsx';
import AccountPage from './Pages/AccountPage.jsx';
import ExtraoptiesPagina from './Pages/ExtraoptiesPagina';
import LessenPage from './Pages/LessenPage';
import AccountOverzicht from './Pages/AccountOverzicht';
import CommunityPage from './Pages/CommunityPage.jsx';
import VoedingsadviesPage from './Pages/VoedingsadviesPage.jsx';

import AanbodOverzicht from './Components/AanbodOverzicht';

function App() {
    return (
        <div>
            <ScrollNaarBoven />

            <link
                href="https://fonts.googleapis.com/css2?family=Noto+Sans&family=Roboto:wght@700&display=swap"
                rel="stylesheet"
            />

            <Navbar />

            <div className="page-content">
                <Routes>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/locaties" element={<LocatiesPage />} />
                    <Route path="/contact" element={<ContactPage />} />
                    <Route path="/aanbod" element={<AanbodOverzicht />} />
                    <Route path="/aanbod/:sportNaam" element={<DetailPagina />} />
                    <Route path="/aanbod/extraopties" element={<ExtraoptiesPagina />} />
                    <Route path="/inschrijven" element={<InschrijfPage />} />
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/community" element={<CommunityPage />} />

                    <Route
                        path="/lessen/:aanbodId"
                        element={
                            <ProtectedRoute>
                                <LessenPage />
                            </ProtectedRoute>
                        }
                    />

                    <Route
                        path="/account"
                        element={
                            <ProtectedRoute>
                                <AccountPage />
                            </ProtectedRoute>
                        }
                    />

                    <Route
                        path="/account/overzicht"
                        element={
                            <ProtectedRoute>
                                <AccountOverzicht />
                            </ProtectedRoute>
                        }
                    />

                    <Route
                        path="/voedingsadvies"
                        element={
                            <ProtectedRoute>
                                <VoedingsadviesPage />
                            </ProtectedRoute>
                        }
                    />

                    <Route path="*" element={<NotFoundPage />} />
                </Routes>
            </div>
        </div>
    );
}

export default App;