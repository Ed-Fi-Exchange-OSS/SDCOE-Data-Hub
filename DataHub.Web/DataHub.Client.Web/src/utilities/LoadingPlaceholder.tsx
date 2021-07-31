import React from 'react';
import { Spinner } from 'react-bootstrap';

interface Props {
  isLoading?: boolean,
  children?: React.ReactNode
}

const LoadingPlaceholder = ({ isLoading, children }: Props) => {
  return (
    isLoading !== undefined && !isLoading
      ? <>{children}</>
      : (<Spinner animation="border">
			<span className="sr-only">Loading...</span>
		</Spinner>)
	)
}

export default LoadingPlaceholder
