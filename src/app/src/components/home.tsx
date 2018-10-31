import {
  AppBar,
  Button,
  createStyles,
  IconButton,
  Menu,
  MenuItem,
  SwipeableDrawer,
  Theme,
  Toolbar,
  Typography,
  WithStyles,
  withStyles
} from '@material-ui/core';
import AccountCircle from '@material-ui/icons/AccountCircle';
import AddIcon from '@material-ui/icons/Add';
import MenuIcon from '@material-ui/icons/Menu';
import MoreIcon from '@material-ui/icons/More';
import * as React from 'react';

import TimeTrackList from '../containers/time-track-list';
import { ITimeTrackInputModel } from '../models/time-track.input-model';
import AddTimeTrack from './add-time-track';

export interface IHomeProps {
  isMenuOpen: boolean;
  isTimeTrackDrawerOpen: boolean;
}

export interface IHomeDispatches {
  openMenu: () => void;
  closeMenu: () => void;
  openTimeTrackDrawer: () => void;
  closeTimeTrackDrawer: () => void;
  addTimeTrack: (model: ITimeTrackInputModel) => void;
}

interface IHomeState {
  anchorEl?: HTMLElement;
  mobileMoreAnchorEl?: HTMLElement;
}

const styles = (theme: Theme) =>
  createStyles({
    heroUnit: {
      backgroundColor: theme.palette.background.paper
    },
    grow: {
      flexGrow: 1
    },
    fabButton: {
      position: 'absolute',
      zIndex: 1,
      bottom: -30,
      left: 0,
      right: 0,
      margin: '0 auto'
    },
    menuButton: {
      marginLeft: -12,
      marginRight: 20
    },
    heroContent: {
      maxWidth: 600,
      margin: '0 auto',
      padding: `${theme.spacing.unit * 8}px 0 ${theme.spacing.unit * 6}px`
    },
    layout: {
      width: 'auto',
      marginLeft: theme.spacing.unit * 3,
      marginRight: theme.spacing.unit * 3,
      [theme.breakpoints.up(1100 + theme.spacing.unit * 3 * 2)]: {
        width: 1100,
        marginLeft: 'auto',
        marginRight: 'auto'
      }
    },
    footer: {
      backgroundColor: theme.palette.background.paper,
      padding: theme.spacing.unit * 6
    },
    sectionDesktop: {
      display: 'none',
      [theme.breakpoints.up('md')]: {
        display: 'flex'
      }
    },
    sectionMobile: {
      display: 'flex',
      [theme.breakpoints.up('md')]: {
        display: 'none'
      }
    },
    title: {
      display: 'none',
      [theme.breakpoints.up('sm')]: {
        display: 'block'
      }
    }
  });

class Home extends React.Component<
  IHomeProps & IHomeDispatches & WithStyles<typeof styles>,
  IHomeState
> {
  public state: IHomeState = {
    anchorEl: undefined,
    mobileMoreAnchorEl: undefined
  };

  public render() {
    const { classes, isMenuOpen, isTimeTrackDrawerOpen } = this.props;
    const { anchorEl, mobileMoreAnchorEl } = this.state;

    const renderMenu = (
      <Menu
        anchorEl={anchorEl}
        anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
        open={isMenuOpen}
        onClose={this.handleMenuClose}
      >
        <MenuItem onClick={this.handleMenuClose}>Profile</MenuItem>
        <MenuItem onClick={this.handleMenuClose}>My account</MenuItem>
      </Menu>
    );

    const renderMobileMenu = (
      <Menu
        anchorEl={mobileMoreAnchorEl}
        anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
        open={isMenuOpen}
        onClose={this.handleMobileMenuClose}
      >
        <MenuItem onClick={this.handleProfileMenuOpen}>
          <IconButton color="inherit">
            <AccountCircle />
          </IconButton>
          <p>Profile</p>
        </MenuItem>
      </Menu>
    );

    return (
      <div>
        <AppBar position="static">
          <Toolbar>
            <Button
              variant="fab"
              color="secondary"
              aria-label="Add"
              className={classes.fabButton}
              onClick={this.handleOpenTimeTrackDrawer}
            >
              <AddIcon />
            </Button>
            <IconButton
              className={classes.menuButton}
              color="inherit"
              aria-label="Open drawer"
            >
              <MenuIcon />
            </IconButton>
            <Typography
              className={classes.title}
              variant="h6"
              color="inherit"
              noWrap={true}
            >
              time-tracker
            </Typography>
            <div className={classes.grow} />
            <div className={classes.sectionDesktop}>
              <IconButton
                aria-owns={isMenuOpen ? 'material-appbar' : undefined}
                aria-haspopup="true"
                onClick={this.handleProfileMenuOpen}
                color="inherit"
              >
                <AccountCircle />
              </IconButton>
            </div>
            <div className={classes.sectionMobile}>
              <IconButton
                aria-haspopup="true"
                onClick={this.handleMobileMenuOpen}
                color="inherit"
              >
                <MoreIcon />
              </IconButton>
            </div>
          </Toolbar>
        </AppBar>
        {renderMenu}
        {renderMobileMenu}
        <SwipeableDrawer
          anchor="right"
          open={isTimeTrackDrawerOpen}
          onOpen={this.handleOpenTimeTrackDrawer}
          onClose={this.handleCloseTimeTrackDrawer}
        >
          <AddTimeTrack addTimeTrack={this.handleAddTimeTrack} />
        </SwipeableDrawer>
        <main>
          <TimeTrackList />
        </main>
        <footer className={classes.footer}>
          <Typography
            variant="subtitle1"
            align="center"
            color="textSecondary"
            component="p"
          >
            time-tracker
          </Typography>
        </footer>
      </div>
    );
  }

  private handleProfileMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    this.setState({ anchorEl: event.currentTarget });

    this.props.openMenu();
  };

  private handleMenuClose = () => {
    this.setState({ anchorEl: undefined });
    this.handleMobileMenuClose();

    this.props.closeMenu();
  };

  private handleMobileMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    this.setState({ mobileMoreAnchorEl: event.currentTarget });

    this.props.openMenu();
  };

  private handleMobileMenuClose = () => {
    this.setState({ mobileMoreAnchorEl: undefined });

    this.props.closeMenu();
  };

  private handleOpenTimeTrackDrawer = () => {
    this.props.openTimeTrackDrawer();
  };

  private handleCloseTimeTrackDrawer = () => {
    this.props.closeTimeTrackDrawer();
  };

  private handleAddTimeTrack = (value: ITimeTrackInputModel) => {
    this.props.addTimeTrack(value);
  };
}

export default withStyles(styles)(Home);
