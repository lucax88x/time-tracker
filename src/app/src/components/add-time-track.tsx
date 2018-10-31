import {
  Avatar,
  Button,
  createStyles,
  FormControl,
  FormControlLabel,
  FormGroup,
  FormLabel,
  Radio,
  RadioGroup,
  Theme,
  WithStyles,
  withStyles
} from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';
import { Formik } from 'formik';
import { DateTime } from 'luxon';
import { InlineDateTimePicker } from 'material-ui-pickers';
import * as React from 'react';

import { UUID } from '../code/uuid';
import { TimeTrackType } from '../models/time-track';
import { ITimeTrackInputModel } from '../models/time-track.input-model';

export interface IAddTimeTrackDispatches {
  addTimeTrack: (value: ITimeTrackInputModel) => void;
}

const flex = {
  display: 'flex',
  alignItems: 'baseline',
  justifyContent: 'space-evenly'
};

const styles = (theme: Theme) =>
  createStyles({
    container: {
      ...flex,
      flexWrap: 'wrap',
      padding: 25
    },
    textField: {
      marginLeft: theme.spacing.unit,
      marginRight: theme.spacing.unit,
      width: 200
    },
    avatar: {
      margin: 10,
      width: 60,
      height: 60
    }
  });

class AddTimeTrack extends React.Component<
  IAddTimeTrackDispatches & WithStyles<typeof styles>
> {
  public render() {
    console.log(TimeTrackType.IN);
    console.log(TimeTrackType[TimeTrackType.IN]);
    const { classes } = this.props;

    return (
      <Formik
        initialValues={{
          id: UUID.Empty,
          when: DateTime.local().toISO(),
          type: TimeTrackType[TimeTrackType.IN]
        }}
        onSubmit={this.handleSubmit}
      >
        {props => {
          const {
            values,
            isSubmitting,
            handleChange,
            handleBlur,
            handleSubmit
          } = props;
          return (
            <form
              className={classes.container}
              noValidate={true}
              onSubmit={handleSubmit}
            >
              <FormControl>
                <FormLabel>Add</FormLabel>
                <FormLabel>
                  <Avatar
                    alt="Adelle Charles"
                    src="/static/images/uxceo-128.jpg"
                    className={classes.avatar}
                  />
                </FormLabel>
                <FormGroup>
                  <RadioGroup
                    aria-label="Tyoe"
                    name="type"
                    value={values.type.toString()}
                    onChange={handleChange}
                    onBlur={handleBlur}
                  >
                    <FormControlLabel
                      value={TimeTrackType[TimeTrackType.IN]}
                      control={<Radio />}
                      label="In"
                    />
                    <FormControlLabel
                      value={TimeTrackType[TimeTrackType.OUT]}
                      control={<Radio />}
                      label="Out"
                    />
                  </RadioGroup>
                  <InlineDateTimePicker
                    autoOk={true}
                    ampm={false}
                    value={values.when}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    label="24h clock"
                  />
                </FormGroup>
                <hr />
                <Button
                  variant="extendedFab"
                  aria-label="Add"
                  type="submit"
                  disabled={isSubmitting}
                >
                  <AddIcon />
                  Save
                </Button>
              </FormControl>
            </form>
          );
        }}
      </Formik>
    );
  }

  private handleSubmit = (value: ITimeTrackInputModel) => {
    this.props.addTimeTrack(value);
  };
}

export default withStyles(styles)(AddTimeTrack);
