import React from 'react';

import { useParams } from 'react-router';

const ShortUrlInfo = () => {
  const { id } = useParams();
  return <div>Short Url Info - ID : {id}</div>;
};

export default ShortUrlInfo;
