import React from 'react';

import { Box, Typography } from '@mui/material';

const Footer = () => {
  return (
    <Box
      component="footer"
      sx={{ p: 2, textAlign: 'center', mt: 'auto', backgroundColor: 'f5f5f5' }}
    >
      <Typography variant="body2">@ 2025 URL Shortener. All rights reserved.</Typography>
    </Box>
  );
};

export default Footer;
