import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import { observer } from "mobx-react";
import React, { useEffect } from "react";

import { useUserStore } from "../stores";

interface Props {
  children?: React.ReactNode;
  withPermissions?: string[];
  showIfNotMet?: React.ReactNode;
}

const RequireUser = observer((props: Props) => {
  const { me, getMe } = useUserStore();

  useEffect(() => {
    getMe();
  }, [getMe]);

  const authMet = me !== null && props && (props.withPermissions === undefined || (props.withPermissions && me.permissions && props.withPermissions.every(p => me.permissions.includes(p))));

  return (
  <>
    <AuthenticatedTemplate>
      {authMet && props.children}
      {!authMet && props.showIfNotMet}
    </AuthenticatedTemplate>
    <UnauthenticatedTemplate></UnauthenticatedTemplate>
  </>);
});

export default RequireUser;
