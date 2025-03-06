import { useState } from 'react';

import { Route, BrowserRouter as Router, Routes } from 'react-router';

import Footer from './components/Footer';
import Header from './components/Header';
import About from './pages/About';
import Login from './pages/Login';
import Register from './pages/Register';
import ShortUrlInfo from './pages/ShortUrlInfo';
import ShortUrlsTable from './pages/ShortUrlsTable';

function App() {
  return (
    <Router>
      <Header />
      <Routes>
        <Route path="/" element={<ShortUrlsTable />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/short-urls/:id" element={<ShortUrlInfo />} />
        <Route path="/about" element={<About />} />
      </Routes>
      <Footer />
    </Router>
  );
}

export default App;
