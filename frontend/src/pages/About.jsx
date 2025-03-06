import React, { useEffect, useState } from 'react';

import {
  Alert,
  Box,
  Button,
  Container,
  Paper,
  TextField,
  Typography,
} from '@mui/material';

import { useAuth } from '../context/AuthContext';
import AlgorithmService from '../service/AlgorithmService';

const About = () => {
  const { user } = useAuth();
  const isAdmin = user?.role === 'Admin';
  const [description, setDescription] = useState('');
  const [error, setError] = useState('');

  useEffect(() => {
    fetchAlgorithmInfo();
  }, []);

  const fetchAlgorithmInfo = async () => {
    try {
      const response = await AlgorithmService.get();
      setDescription(response.algohithmInfo?.description || '');
    } catch (error) {
      console.error('Error fetching algorithm description:', error);
      setError('Failed to load the description.');
    }
  };

  const handleSave = async () => {
    if (!description.trim()) {
      setError('Description cannot be empty.');
      return;
    }

    try {
      await AlgorithmService.update({ description });
      setError('');
    } catch (error) {
      console.error('Error saving description:', error);
      setError('Failed to save the description.');
    }
  };

  return (
    <Container maxWidth="md">
      <Paper sx={{ padding: 3, mt: 5 }}>
        <Typography variant="h4" align="center" gutterBottom>
          About URL Shortener Algorithm
        </Typography>

        {error && <Alert severity="error">{error}</Alert>}

        {isAdmin ? (
          <Box
            component="form"
            sx={{ display: 'flex', flexDirection: 'column', gap: 2, mt: 2 }}
          >
            <TextField
              label="Algorithm Description"
              multiline
              rows={5}
              variant="outlined"
              fullWidth
              value={description}
              onChange={e => setDescription(e.target.value)}
            />
            <Button variant="contained" color="primary" onClick={handleSave}>
              Save
            </Button>
          </Box>
        ) : (
          <Typography variant="body1" sx={{ mt: 2 }}>
            {description || 'No description available.'}
          </Typography>
        )}
      </Paper>
    </Container>
  );
};

export default About;
