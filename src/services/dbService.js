import { Pool } from 'pg';

import dbConfig from '../configs/dbConfig';

const pool = new Pool(dbConfig);

export default pool;
