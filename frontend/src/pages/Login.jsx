import React, { useState } from 'react';

import { Alert, Button, TextField, Typography } from '@mui/material';
import { Box, Container } from '@mui/system';

import AuthService from '../service/AuthService';

const Login = () => {
  const [form, setForm] = useState({
    username: '',
    password: '',
  });

  const [error, setError] = useState('');

  const handleChange = e => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async e => {
    e.preventDefault();
    setError('');

    try {
      const response = await AuthService.login(form);
      console.log('Response:', response);
    } catch (error) {
      setError(error.message);
    }
  };

  return (
    <Container maxWidth="xs">
      <Box
        component="form"
        onSubmit={handleSubmit}
        sx={{ mt: 8, display: 'flex', flexDirection: 'column', gap: 2 }}
      >
        <Typography variant="h5" align="center">
          Login
        </Typography>
        <TextField
          label="Username"
          variant="outlined"
          fullWidth
          name="username"
          value={form.username}
          onChange={handleChange}
          required
        />
        <TextField
          label="Password"
          type="password"
          variant="outlined"
          fullWidth
          name="password"
          value={form.password}
          onChange={handleChange}
          required
        />
        <Button type="submit" variant="contained" color="primary" fullWidth>
          Login
        </Button>
        {error && <Alert severity="error">{error}</Alert>}
      </Box>
    </Container>
  );
};

export default Login;
