import { NavLink } from 'react-router';

import { AppBar, Box, Button, Link, Toolbar, Typography } from '@mui/material';

const Header = () => {
  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" sx={{ flexGrow: 1 }}>
          <Link
            component={NavLink}
            to="/"
            style={{ textDecoration: 'none', color: 'inherit' }}
          >
            Url Shortener
          </Link>
        </Typography>
        <Box>
          <Button component={NavLink} to="/about" color="inherit">
            About
          </Button>
          <Button component={NavLink} to="/login" color="inherit">
            Login
          </Button>
          <Button component={NavLink} to="/register" color="inherit">
            Register
          </Button>
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default Header;
