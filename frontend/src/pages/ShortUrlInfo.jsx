import React, { useEffect, useState } from 'react';

import { useNavigate, useParams } from 'react-router';

import { Paper, Typography } from '@mui/material';

import UrlService from '../service/UrlService';

const UrlDetails = () => {
  const { id } = useParams();
  const [url, setUrl] = useState(null);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (!token) {
      navigate('/login');
    } else {
      fetchUrlDetails();
    }
  }, [navigate]);

  const fetchUrlDetails = async () => {
    try {
      const response = await UrlService.getById(id);
      setUrl(response.url);
    } catch (error) {
      console.error('Error fetching URL details:', error);
      setError(error.message || 'Failed to fetch URL details');
    }
  };

  if (error) {
    return <Typography color="error">{error}</Typography>;
  }

  if (!url) {
    return <Typography>URL not found</Typography>;
  }

  return (
    <Paper style={{ padding: '1rem' }}>
      <Typography variant="h4">URL Details</Typography>
      <Typography>
        <strong>Original URL:</strong> {url.originalUrl}
      </Typography>
      <Typography>
        <strong>Shortened URL:</strong> {url.shortUrl}
      </Typography>
      <Typography>
        <strong>Created By:</strong> {url.createdBy}
      </Typography>
      <Typography>
        <strong>Created Date:</strong> {new Date(url.createdAt).toLocaleString()}
      </Typography>
    </Paper>
  );
};

export default UrlDetails;
