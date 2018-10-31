import {
  Button,
  createStyles,
  FormControl,
  FormControlLabel,
  FormGroup,
  Radio,
  RadioGroup,
  Theme,
  WithStyles,
  withStyles
} from '@material-ui/core';
import SaveIcon from '@material-ui/icons/Save';
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
    radioGroup: {
      flexDirection: 'row'
    },
    textField: {
      marginLeft: theme.spacing.unit,
      marginRight: theme.spacing.unit,
      width: 200
    }
  });

class AddTimeTrack extends React.Component<
  IAddTimeTrackDispatches & WithStyles<typeof styles>
> {
  public render() {
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
                <FormGroup>
                  <RadioGroup
                    className={classes.radioGroup}
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
                  />
                </FormGroup>
                <hr />
                <Button
                  variant="extendedFab"
                  aria-label="Save"
                  color="primary"
                  type="submit"
                  disabled={isSubmitting}
                >
                  <SaveIcon />
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
