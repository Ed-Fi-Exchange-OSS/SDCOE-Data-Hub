import { observer } from 'mobx-react-lite';
import { fromPromise, IBasePromiseBasedObservable } from 'mobx-utils';
import { FunctionComponent, ReactElement, useEffect, useState } from 'react';

import { useOnMount } from './useOnMount';

export interface Props {
	promise?: Promise<any>;
	promiseFn?: (...args: any) => Promise<any>;
	params?: any[];
	pending?: () => ReactElement | null;
	rejected?: (err: Error) => ReactElement | null;
	fulfilled: (resp: any) => ReactElement | null;
}

const dummyPromise: IBasePromiseBasedObservable<Object> = fromPromise(new Promise(() => { }));

export const Async: FunctionComponent<Props> = observer(
	({
		promise,
		promiseFn,
		params = [],
		pending = () => null,
		rejected = () => null,
		fulfilled
	}: Props) => {
		if (promise && promiseFn) {
			throw new Error('Cannot specify both promise and promiseFn');
		}
		if (!promise && !promiseFn) {
			throw new Error('Must specify either promise or promiseFn');
		}

		const [promiseResult, setPromise] = useState(dummyPromise);

		if (promise) {
			useEffect(() => {
				setPromise(fromPromise(promise));
			}, [promise]);
		} else if (promiseFn) {
			useOnMount(() => {
				setPromise(fromPromise(promiseFn.apply(null, params)));
			});
		}

		return promiseResult.case({
			pending,
			rejected: (err: Error) => rejected(err),
			fulfilled: (resp: any) => fulfilled(resp)
		});
	},
);
