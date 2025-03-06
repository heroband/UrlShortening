import React, { useEffect, useState } from 'react';

import { useNavigate } from 'react-router';

import {
  Button,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TextField,
} from '@mui/material';

import { useAuth } from '../context/AuthContext';
import UrlService from '../service/UrlService';

const ShortUrlsTable = () => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [urls, setUrls] = useState([]);
  const [newUrl, setNewUrl] = useState('');
  const [error, setError] = useState('');

  useEffect(() => {
    fetchUrls();
  }, []);

  const fetchUrls = async () => {
    try {
      const data = await UrlService.getAll();
      setUrls(data || []);
    } catch (error) {
      console.error('Error fetching URLs:', error);
      setUrls([]);
    }
  };

  const handleAddUrl = async () => {
    if (!newUrl.trim()) return;
    setError('');

    try {
      await UrlService.create({ OriginalUrl: newUrl });
      setNewUrl('');
      fetchUrls();
    } catch (error) {
      setError(error.message);
    }
  };

  const handleDelete = async id => {
    try {
      await UrlService.delete(id);
      fetchUrls();
    } catch (error) {
      console.error('Error deleting URL:', error);
    }
  };

  const handleRedirect = async shortUrl => {
    try {
      const response = await UrlService.redirect(shortUrl);

      if (response && response.message) {
        window.location.href = response.message;
      }
    } catch (error) {
      console.error('Error redirecting:', error);
    }
  };

  return (
    <div>
      <h2>Shortened URLs List</h2>

      {user && (
        <div style={{ marginBottom: '1rem' }}>
          <TextField
            label="Enter URL"
            variant="outlined"
            value={newUrl}
            onChange={e => setNewUrl(e.target.value)}
            size="small"
            style={{ marginRight: '10px' }}
          />
          <Button variant="contained" color="primary" onClick={handleAddUrl}>
            Add
          </Button>
          {error && <p style={{ color: 'red' }}>{error}</p>}
        </div>
      )}

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Original URL</TableCell>
              <TableCell>Shortened URL</TableCell>
              <TableCell>Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {urls.map(url => (
              <TableRow key={url.id}>
                <TableCell>{url.originalUrl}</TableCell>
                <TableCell>
                  <a
                    href={`#`}
                    onClick={e => {
                      e.preventDefault();
                      handleRedirect(url.shortUrl);
                    }}
                  >
                    heroband/{url.shortUrl}
                  </a>
                </TableCell>
                <TableCell>
                  {user && (
                    <Button
                      variant="contained"
                      color="info"
                      onClick={() => navigate(`/short-urls/${url.id}`)}
                    >
                      Info
                    </Button>
                  )}
                  {user && (user.role === 'Admin' || user.id === url.createdBy) && (
                    <Button
                      variant="contained"
                      color="error"
                      onClick={() => handleDelete(url.id)}
                      style={{ marginLeft: '10px' }}
                    >
                      Delete
                    </Button>
                  )}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};

export default ShortUrlsTable;
